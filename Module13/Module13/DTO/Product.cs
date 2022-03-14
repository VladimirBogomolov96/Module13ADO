using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module13.DTO
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public Product()
        {
        }

        public Product(string name, string description, int weight, int height, int width, int length) : this(0, name, description, weight, height, width, length)
        {
        }

        public Product(int id, string name, string description, int weight, int height, int width, int length)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Weight = weight;
            this.Height = height;
            this.Width = width;
            this.Length = length;
        }
    }
}
