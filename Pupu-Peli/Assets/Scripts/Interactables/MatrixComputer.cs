using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatrixComputer : ComputerInteractable
{
    public int exitTimer;

    public override void ExitComputer()
    {

        FindAnyObjectByType<MatrixGameManager>().GameEndOnExit();

        StartCoroutine(WaitBeforeExit());

    }

    public IEnumerator WaitBeforeExit() {
    
        yield return new WaitForSeconds(exitTimer);

        base.ExitComputer();
    }
}
