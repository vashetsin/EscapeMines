using Application.Core.Services.MineFields;
using Application.Core.Services.Tiles;
using Application.Core.Services.Turtles;
using Domain.Core.Enums;
using Infrastructure.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class App
    {
        private readonly IMineFieldService _mineFieldService;
        private readonly ITurtleService _turtleService;
        private readonly ITileService _tileService;

        public App(
            IMineFieldService mineFieldService,
            ITurtleService turtleService,
            ITileService tileService)
        {
            _mineFieldService = mineFieldService;
            _turtleService = turtleService;
            _tileService = tileService;
        }

        public void Run()
        {
            try
            {
                var mineField = _mineFieldService.InitMineField();
                var turtleExtended = _turtleService.InitTurtle(mineField);
                var turtle = turtleExtended.Turtle;
                var moves = turtleExtended.Moves.ToArray();

                var result = ResultType.StillInDanger;
                for (int i = 0; i < moves.Length; i++)
                {
                    var move = moves[i];

                    /*
                       Console.Output of current picture if needed
                   */
                    _turtleService.TryMoveTurtle(turtle, move, mineField.MaxPositionX, mineField.MaxPositionY);

                    var currentTyle = mineField[turtle.CurrentPositionX, turtle.CurrentPositionY];
                    result = _tileService.GetResult(currentTyle);

                    if (result != ResultType.StillInDanger)
                    {
                        break;
                    }
                }

                Console.WriteLine(result);
            }
            catch (InvalidSettingsException)
            {
                Console.WriteLine("Parse settings failed.");
            }
            catch (MineFieldInitializationException)
            {
                Console.WriteLine("Wrong settings.");
            }
            catch (TurtleOutOfFieldException)
            {
                Console.WriteLine("Turtle is out of the field.");
            }
            catch (TurtleInitialTileException)
            {
                Console.WriteLine("Turtle must be on an empty tile in the beginning.");
            }
            catch (Exception)
            {
                Console.WriteLine("Oops, something went wrong...");
            }
            Console.ReadKey();
        }
    }
}
