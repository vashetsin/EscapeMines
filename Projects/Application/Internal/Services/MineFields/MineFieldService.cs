using Application.Core.Services.MineFields;
using Application.Internal.Services.MineFields.Validator;
using Infrastructure.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Internal.Services.MineFields
{
    internal class MineFieldService : IMineFieldService
    {
        private readonly IMineFieldRepository _mineFieldRepository;
        private readonly ITileRepository _tileRepository;
        private readonly MineFieldValidator _validator;

        public MineFieldService(
            IMineFieldRepository mineFieldRepository,
            ITileRepository tileRepository,
            MineFieldValidator validator)
        {
            _mineFieldRepository = mineFieldRepository;
            _tileRepository = tileRepository;
            _validator = validator;
        }

        public IMineFieldExtended InitMineField()
        {
            var mineField = _mineFieldRepository.GetMineField();
            var exit = _tileRepository.GetExit();
            var mines = _tileRepository.GetMines();

            _validator.ValidateMineField(mineField);
            _validator.ValidateExit(mineField, exit);
            _validator.ValidateMines(mineField, mines);

            return new MineFieldExtended(mineField, exit, mines);
        }
    }
}
