using CiotNetNS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CiotNetNS.Application.DTOs
{
    public class InterfaceInfoDto
    {
        public InterfaceType Type { get; set; }
        
        public byte Id { get; set; }

        public InterfaceInfoDto(InterfaceType type = InterfaceType.Unknown, byte id = 0) 
        {
            Type = type;
            Id = id;
        }
    }
}
