using BaseRPG.View.Interfaces;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Image
{
    internal class DefaultWorldNameImageMapper : IWorldNameImageMapper
    {
        private Dictionary<string, string> mapping = new();

        public DefaultWorldNameImageMapper()
        {
            mapping.Add("Empty",@"Assets\image\bacground\big-background-mozaic.jpg");
        }

        public string ToImageName(string worldName)
        {
            return mapping[worldName];
        }
    }
}
