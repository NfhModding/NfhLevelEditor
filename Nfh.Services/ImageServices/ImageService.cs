﻿using Image.Tga;
using Nfh.Domain.Interfaces;
using System;
using System.Drawing;
using System.IO;

namespace Nfh.Services.ImageServices
{
    internal class ImageService : IImageService
    {
        private readonly IGfxPrepareService gfxPrepareService;

        public ImageService(IGfxPrepareService gfxPrepareService)
        {
            this.gfxPrepareService = gfxPrepareService;
        }

        public Bitmap LoadLevelThumbnail(string levelId, string gamePath)
        {
            var gfxdataFolder = gfxPrepareService.PrepareGfxData(new(gamePath));
            return LoadImage(new(Path.Combine(gfxdataFolder.FullName, "gui", "main", $"{levelId}.tga")));
        }

        public Bitmap LoadAnimationFrame(string objectId, string frameName, string gamePath)
        {
            var gfxdataFolder = gfxPrepareService.PrepareGfxData(new(gamePath));
            var objectPath = Path.Combine(objectId.Split('/', '\\') ?? Array.Empty<string>());
            return LoadImage(new(Path.Combine(gfxdataFolder.FullName, objectPath, frameName)));
        }    
        
        private static Bitmap LoadImage(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
                throw new($"Image loading: {fileInfo.FullName} does not exists");
            return TgaImage.FromFile(fileInfo.FullName).ToBitmap();
        }
    }
}
