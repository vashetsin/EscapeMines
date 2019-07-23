using AppConfig.Core;
using Domain.Core.Entities;
using Domain.Core.Factories;
using Infrastructure.Core.Persistence;
using Shared.Helpers;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Internal.Persistence
{
    internal class TextFileMineFieldRepository : TextFileRepository, IMineFieldRepository
    {
        private readonly IMineFieldFactory _mineFieldFactory;

        public TextFileMineFieldRepository(
            ISettings settings,
            FileUtility fileUtility,
            IMineFieldFactory mineFieldFactory) : base(settings, fileUtility)
        {
            _mineFieldFactory = mineFieldFactory;
        }

        public MineField GetMineField()
        {
            return Parse(() =>
            {
                var line = ReadLine(1);
                var arr = line.Split(' ');
                Throw.If<Exception>(() => arr.Length != 2);
                var tilesX = int.Parse(arr[0]);
                var tilesY = int.Parse(arr[1]);
                return _mineFieldFactory.Create(tilesX, tilesY);
            });
        }
    }
}
