namespace IntelHexFormatReader.Model
{
    /// <summary>
    /// Logical representation of a MemoryBlock (an ordered collection of memory cells and registers).
    /// </summary>
    public class MemoryBlock
    {
        /// <summary>
        /// Memory offset for microcontrollers
        /// with memory that doesn't start at 0x00000000.
        /// This should save memory from having to populate
        /// vast ranges of unused data.
        /// </summary>
        
        public uint StartAddress { get; set; }
        
        // CS & IP registers for 80x86 systems.

        /// <summary>
        /// Code Segment register (16-bit).
        /// </summary>
        public ushort CS { get; set; }

        /// <summary>
        /// Instruction Pointer register (16-bit).
        /// </summary>
        public ushort IP { get; set; }

        // EIP register for 80386 and higher CPU's.

        /// <summary>
        /// Extended Instruction Pointer register (32-bit).
        /// </summary>
        public uint EIP { get; set; }

        /// <summary>
        /// Returns the index of the highest modified cell.
        /// </summary>
        public int HighestModifiedOffset
        {
            get { return Cells.LastIndexOf(cell => cell.Modified); }
        }

        /// <summary>
        /// Returns the size of this memory, in bytes.
        /// </summary>
        public int MemorySize
        {
            get { return Cells.Length; }
        }

        /// <summary>
        /// Memory cells in this memory block.
        /// </summary>
        public MemoryCell[] Cells { get; set; }

        
        /// <summary>
        /// Construct a new MemoryBlock.
        /// </summary>
        /// <param name="memorySize">The size of the MemoryBlock to instantiate in bytes.</param>
        /// <param name="fillValue">Default cell initialization / fill value.</param>
        /// <param name="baseAddress">Start of memory for this system.</param>
        public MemoryBlock(int memorySize, byte fillValue = 0xff, uint baseAddress = 0)
        {
            StartAddress = baseAddress;
            Cells = new MemoryCell[memorySize];
            for (var i = 0; i < memorySize; i++)
                Cells[i] = new MemoryCell(i) { Value = fillValue };
        }
    }
}
