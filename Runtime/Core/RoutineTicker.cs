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
    private static RoutineTickerComponent component;

    /// <summary>
    /// Anonymous mono behaviour used for ticking enumators.
    /// </summary>
    private class RoutineTickerComponent : MonoBehaviour { }

    /// <summary>
    /// Starts a coroutine using a given enumerator.
    /// </summary>
    /// <param name="routine">The coroutine.</param>
    /// <returns>An enumerator.</returns>
    public static IEnumerator StartCoroutine (IEnumerator routine) {
      if (component == null)
        component = new GameObject ("~RoutineTicker").AddComponent<RoutineTickerComponent> ();
      yield return component.StartCoroutine (routine);
    }
  }
}