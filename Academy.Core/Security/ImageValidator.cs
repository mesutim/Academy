﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;

namespace Academy.Core.Security
{
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                var img =System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
