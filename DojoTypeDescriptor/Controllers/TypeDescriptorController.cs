using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DojoTypeDescriptor.Converters;
using DojoTypeDescriptor.Forms;
using DojoTypeDescriptor.Generators;
using DojoTypeDescriptor.Helpers;
using DojoTypeDescriptor.Models;
using Newtonsoft.Json;

namespace DojoTypeDescriptor.Controllers
{
    [RoutePrefix("typedescriptor")]
    public class TypeDescriptorController : Controller
    {
        // GET: TypeDescriptor
        [Route("{packageName}")]
        [HttpPost]
        public string PostDescriptor(string packageName)
        {
            var sr = new StreamReader(Request.Files[0].InputStream);

            string data = Uri.UnescapeDataString(sr.ReadToEnd());

            DojoObjectForm result = JsonConvert.DeserializeObject<DojoObjectForm>(data);
            DojoObjectForm resultMerged = DojoObjectHelper.MergeChildren(result);
            DojoObjectHelper.BuildParentTree(resultMerged, resultMerged);

            DojoPackage package = DojoPackageConverter.Convert(resultMerged);

            string document = PackageGenerator.Generate(package.Packages.First(p => p.Name.Equals(packageName)));

            byte[] bytes = Encoding.UTF8.GetBytes(document.ToCharArray());

            System.IO.File.WriteAllBytes(Server.MapPath("~") + "Scripts/typings/" + packageName + ".d.ts", bytes);

            return "success";
        }
    }
}