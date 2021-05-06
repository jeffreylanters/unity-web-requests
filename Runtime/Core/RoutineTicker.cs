using System;
using UnityEngine;
using System.Collections;

namespace JeffreyLanters.WebRequests.Core {

  /// <summary>
  /// Anonymous wrapper for a mono behaviour used for ticking enumerators.
  /// </summary>
  public class RoutineTicker {

    /// <summary>
    /// Reference to the routine ticker component instance, this component will
    /// be used for housing the web request enumerators.
    /// </summary>
    private static RoutineTickerComponent routineTickerComponent = null;

    /// <summary>
    /// Anonymous mono behaviour used for ticking enumators.
    /// </summary>
    private class RoutineTickerComponent : MonoBehaviour {

      /// <summary>
      /// Starts a coroutine using a given enumerator and will invoke a complete
      /// event handler.
      /// </summary>
      /// <param name="routine">The coroutine.</param>
      /// <returns>An enumerator.</returns>
      public IEnumerator StartCompletableCoroutine (IEnumerator routine, Action onComplete) {
        yield return this.StartCoroutine (routine);
        onComplete ();
      }
    }

    /// <summary>
    /// Starts a coroutine using a given enumerator and will invoke a complete
    /// event handler.
    /// </summary>
    /// <param name="routine">The coroutine.</param>
    /// <returns>An enumerator.</returns>
    public static void StartCompletableCoroutine (IEnumerator routine, Action onComplete) {
      if (RoutineTicker.routineTickerComponent == null)
        RoutineTicker.routineTickerComponent = new GameObject ("~RoutineTicker").AddComponent<RoutineTickerComponent> ();
      RoutineTicker.routineTickerComponent.StartCoroutine (RoutineTicker.routineTickerComponent.StartCompletableCoroutine (routine, onComplete));
    }
  }
}