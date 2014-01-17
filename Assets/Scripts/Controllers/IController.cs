using UnityEngine;
using System.Collections;

namespace Surge.Controllers
{
    public interface IController
    {
        //return true when controller is fully initialized and ready to start game
        bool ReadyToStartGame();
    }
}
