using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace mumblelib
{
    public class MumbleLinkFile : IDisposable
    {
        public const string MapName = "MumbleLink";

        /// <summary>Holds a reference to the shared memory block.</summary>
        private readonly MemoryMappedFile memoryMappedFile;

        /// <summary>Indicates whether this object is disposed.</summary>
        private bool disposed;

        public MumbleLinkFile(MemoryMappedFile memoryMappedFile)
        {
            this.memoryMappedFile = memoryMappedFile ?? throw new ArgumentNullException("memoryMappedFile");
        }

        /// <summary>
        ///     Creates or opens a memory-mapped file for the MumbleLink protocol.
        /// </summary>
        /// <returns>An object that provides wrapper methods for the MumbleLink protocol.</returns>
        public static MumbleLinkFile CreateOrOpen()
        {
            return new MumbleLinkFile(MemoryMappedFile.CreateOrOpen(MapName, Marshal.SizeOf<Avatar>()));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public byte[] ReadRaw()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            using (var stream = memoryMappedFile.CreateViewStream())
            {
                // Copy the shared memory block to a local buffer in managed memory
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>Retrieves positional audio data from the shared memory block as defined by the Mumble Link protocol.</summary>
        /// <returns>Positional audio data.</returns>
        public Avatar Read()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
            using (var stream = memoryMappedFile.CreateViewStream())
            {
                // Copy the shared memory block to a local buffer in managed memory
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                // Pin the managed memory so that the GC doesn't move it
                var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

                // Get the address of the managed memory
                var ptr = handle.AddrOfPinnedObject();
                Avatar avatar;
                try
                {
                    // Copy the managed memory to a managed struct
                    avatar = (Avatar)Marshal.PtrToStructure(ptr, typeof(Avatar));
                }
                finally
                {
                    // Release the handle
                    handle.Free();
                }

                return avatar;
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing"><c>true</c> if managed resources should be released.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                memoryMappedFile.Dispose();
            }
            disposed = true;
        }
        public void write(byte[] towrite)
        {
            using (var stream = memoryMappedFile.CreateViewStream())
            {
                stream.Write(towrite, 0, towrite.Length);
            }
        }
    }
   
}
