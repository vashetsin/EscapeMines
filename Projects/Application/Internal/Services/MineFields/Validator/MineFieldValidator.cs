using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Services.MineFields;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Shared.Helpers;

namespace Application.Internal.Services.MineFields.Validator
{
    internal class MineFieldValidator
    {
        internal virtual void ValidateMines(MineField mineField, IEnumerable<Tile> mines)
        {
            mines.ToList().ForEach(x =>
            {
                Throw.If<MineFieldInitializationException>(() =>
                   x.PositionX < 0 || x.PositionY < 0 || x.TileType != TileType.Mine);
                Throw.If<MineFieldInitializationException>(() =>
                    x.PositionX >= mineField.TilesX || x.PositionY >= mineField.TilesY);
            });
        }

        internal virtual void ValidateExit(MineField mineField, Tile exit)
        {
            Throw.If<MineFieldInitializationException>(() =>
                exit.PositionX < 0 || exit.PositionY < 0 || exit.TileType != TileType.Exit);
            Throw.If<MineFieldInitializationException>(() =>
                exit.PositionX >= mineField.TilesX || exit.PositionY >= mineField.TilesY);
        }

        internal virtual void ValidateMineField(MineField mineField)
        {
            Throw.If<MineFieldInitializationException>(() =>
                mineField.TilesX < 2 || mineField.TilesY < 2);
        }
    }
}
