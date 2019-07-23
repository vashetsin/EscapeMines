using AppConfig.Core;
using Domain.Core.Entities;
using Domain.Core.Enums;
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
    internal class TextFileTileRepository : TextFileRepository, ITileRepository
    {
        private readonly ITileFactory _tileFactory;

        public TextFileTileRepository(
            ISettings settings,
            FileUtility fileUtility,
            ITileFactory tileFactory) : base(settings, fileUtility)
        {
            _tileFactory = tileFactory;
        }

        public Tile GetExit()
        {
            return Parse(() =>
            {
                var line = ReadLine(3);
                var arr = line.Split(' ');
                Throw.If<Exception>(() => arr.Length != 2);
                var positionX = int.Parse(arr[0]);
                var positionY = int.Parse(arr[1]);
                return _tileFactory.Create(positionX, positionY, TileType.Exit);
            });
        }

        public IEnumerable<Tile> GetMines()
        {
            return Parse(() =>
            {
                var retVal = new List<Tile>();

                var line = ReadLine(2);
                line.Split(' ').ToList().ForEach(x =>
                {
                    var arr = x.Split(',');
                    Throw.If<Exception>(() => arr.Length != 2);
                    var positionX = int.Parse(arr[0]);
                    var positionY = int.Parse(arr[1]);
                    retVal.Add(_tileFactory.Create(positionX, positionY, TileType.Mine));
                });

                return retVal;
            });
        }
    }
}
