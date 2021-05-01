using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using imgLoader_WPF.Windows;
using imgL_Sites;

namespace imgLoader_WPF.Services
{
    internal class Categorizer
    {
        private readonly string _catFile = Core.FilesRoute + "Cat";

        private ImgLoader _sender;

        internal List<(string, string)> CategoryList = new();

        public Categorizer(ImgLoader sender)
        {
            _sender = sender;

            if (!File.Exists(_catFile)) return;

            var content = File.ReadAllText(_catFile);

            foreach (var cat in content.Split("},", StringSplitOptions.RemoveEmptyEntries))
            {
                CategoryList.Add((cat.Split(':')[0], cat.Split('{')[1]));
            }
        }

        internal void AddCategory()
        {

        }

        internal void RemoveCategory()
        {

        }

        internal void AddToCategory()
        {

        }

        internal void RemoveFromCategory()
        {

        }

        internal void SaveToFile()
        {
            var sb = new StringBuilder();

            foreach (var (catName, numbers) in CategoryList)
            {
                sb.Append(catName).Append(":{").Append(numbers).Append("},");
            }

            File.WriteAllText(_catFile, sb.ToString());
        }
    }
}
