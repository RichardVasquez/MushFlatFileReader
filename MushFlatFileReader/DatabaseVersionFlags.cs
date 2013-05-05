using System;

namespace MushFlatFileReader
{
	[Flags]
	public enum DatabaseVersionFlags : long
	{
		/// <summary>
		/// Empty
		/// </summary>
		None = 0,
		/// <summary>
		/// Database version
		/// </summary>
		Mask = 0x000000ff,
		/// <summary>
		/// ZONE/DOMAIN field
		/// </summary>
		Zone = 0x00000100,
		/// <summary>
		/// LINK field (exits from objs)
		/// </summary>
		Link = 0x00000200,
		///<summary>
		/// attrs are in a gdbm db, not here
		/// </summary>
		Gdbm = 0x00000400,
		/// <summary>
		/// NAME is an attr, not in the hdr
		/// </summary>
		AtrName = 0x00000800,
		/// <summary>
		/// KEY is an attr, not in the hdr
		/// </summary>
		AtrKey = 0x00001000,
		/// <summary>
		/// PERN: Extra locks in object hdr
		/// </summary>
		PernKey = 0x00001000,
		/// <summary>
		/// db has the PARENT field
		/// </summary>
		Parent = 0x00002000,
		/// <summary>
		/// PERN: Comm status in header
		/// </summary>
		Comm = 0x00004000,
		/// <summary>
		/// Money is kept in an attribute
		/// </summary>
		AtrMoney = 0x00008000,
		/// <summary>
		/// An extra word of flags
		/// </summary>
		Xflags = 0x00010000,
		/// <summary>
		/// Powers?
		/// </summary>
		Powers = 0x00020000,
		/// <summary>
		/// Adding a 3rd flag word
		/// </summary>
		Flags3 = 0x00040000,
		/// <summary>
		/// Quoted strings, ala PennMUSH
		/// </summary>
		Quoted = 0x00080000,
		/// <summary>
		/// Typed quotas
		/// </summary>
		Tquotas = 0x00100000,
		/// <summary>
		/// Timestamps
		/// </summary>
		Timestamps = 0x00200000,
		/// <summary>
		/// ODark-to-Visual attr flags
		/// </summary>
		VisualAttrs = 0x00400000,
		/// <summary>
		/// Option to clean attr table
		/// </summary>
		DbClean = 0x80000000
	}
}