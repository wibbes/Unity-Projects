﻿/*
 * Copyright 2015 Google Inc. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Google.Cast.RemoteDisplay;

/**
 * Manages the camera used for the mobile and the remote Cast device.
 * The mobile camera will toggle when casting.
 */
public class RemoteCameraManager : MonoBehaviour {

  /**
   * Reference to the display manager.
   */
  public CastRemoteDisplayManager displayManager;

  /**
   * Used to render graphics on the mobile display.
   */
  public Camera RemoteDisplayCamera;

  /**
   * Used to render graphics on the remote display.
   */
  public Camera MainCamera;

  /**
  * Listen to the CastRemoteDisplayManager events.
  */
  void Start() {
    if (!displayManager) {
      displayManager = CastRemoteDisplayManager.GetInstance();
    }

    if (!displayManager) {
      Debug.LogError("DebugCastUIController ERROR: No CastRemoteDisplayManager found!");
      Destroy(gameObject);
      return;
    }

    displayManager.RemoteDisplaySessionStartEvent.AddListener(OnRemoteDisplaySessionStart);
    displayManager.RemoteDisplaySessionEndEvent.AddListener(OnRemoteDisplaySessionEnd);
    displayManager.RemoteDisplayErrorEvent.AddListener(OnRemoteDisplayError);
    if (displayManager.IsCasting()) {
      RemoteDisplayCamera.enabled = true;
      displayManager.RemoteDisplayCamera = MainCamera;
    }

    MainCamera.enabled = true;
  }

  /**
  * Stop listening to the CastRemoteDisplayManager events.
  */
  private void OnDestroy() {
    displayManager.RemoteDisplaySessionStartEvent.RemoveListener(OnRemoteDisplaySessionStart);
    displayManager.RemoteDisplaySessionEndEvent.RemoveListener(OnRemoteDisplaySessionEnd);
    displayManager.RemoteDisplayErrorEvent.RemoveListener(OnRemoteDisplayError);
  }
  

  /**
   * Cast session started, so change the mobile device camera.
   */
  public void OnRemoteDisplaySessionStart(CastRemoteDisplayManager manager) {
    displayManager.RemoteDisplayCamera = MainCamera;
    RemoteDisplayCamera.enabled = true;
  }

  /**
   * Cast session ended, so change the mobile device camera.
   */
  public void OnRemoteDisplaySessionEnd(CastRemoteDisplayManager manager) {
    displayManager.RemoteDisplayCamera = null;
    RemoteDisplayCamera.enabled = false;
    MainCamera.enabled = true;
  }

  /**
   * Handles error messages from the Remote Display Manager.
   */
  public void OnRemoteDisplayError(CastRemoteDisplayManager manager) {
    RemoteDisplayCamera.enabled = false;
    MainCamera.enabled = true;
  }

}
