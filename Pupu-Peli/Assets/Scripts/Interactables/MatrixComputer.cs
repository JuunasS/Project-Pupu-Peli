using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatrixComputer : ComputerInteractable
{
    public int exitTimer;

    public override async void ExitComputer()
    {

        FindAnyObjectByType<MatrixGameManager>().GameEndOnExit();

        await Task.Delay(exitTimer * 1000); // milliseconds -> seconds

        base.ExitComputer();
    }

}
