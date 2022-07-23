//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/UI/Input/UIController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @UIController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @UIController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIController"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""c236d98c-39fa-473b-a03b-3900b7bfbdd4"",
            ""actions"": [
                {
                    ""name"": ""Advance"",
                    ""type"": ""Button"",
                    ""id"": ""dc4ed2f6-d0dd-4306-b422-6d5fca0ba8b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Retract"",
                    ""type"": ""Button"",
                    ""id"": ""3e9a119c-2fe5-4e02-8671-6f9f152d16c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""484a0ed5-3a8a-40f4-9e8f-3917f385dfda"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""UI"",
                    ""action"": ""Advance"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac10317c-26a5-4a5f-9a13-b34fb0683b3c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""UI"",
                    ""action"": ""Advance"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e22e7129-8aed-43de-b3b3-4cde67e425cf"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""UI"",
                    ""action"": ""Advance"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4dc0c31f-a480-4839-8f62-79f6662a0cd2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""UI"",
                    ""action"": ""Retract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14b1f739-274a-449e-924a-1af0abffd79e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""UI"",
                    ""action"": ""Retract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""UI"",
            ""bindingGroup"": ""UI"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Advance = m_UI.FindAction("Advance", throwIfNotFound: true);
        m_UI_Retract = m_UI.FindAction("Retract", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Advance;
    private readonly InputAction m_UI_Retract;
    public struct UIActions
    {
        private @UIController m_Wrapper;
        public UIActions(@UIController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Advance => m_Wrapper.m_UI_Advance;
        public InputAction @Retract => m_Wrapper.m_UI_Retract;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Advance.started -= m_Wrapper.m_UIActionsCallbackInterface.OnAdvance;
                @Advance.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnAdvance;
                @Advance.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnAdvance;
                @Retract.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRetract;
                @Retract.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRetract;
                @Retract.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRetract;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Advance.started += instance.OnAdvance;
                @Advance.performed += instance.OnAdvance;
                @Advance.canceled += instance.OnAdvance;
                @Retract.started += instance.OnRetract;
                @Retract.performed += instance.OnRetract;
                @Retract.canceled += instance.OnRetract;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_UISchemeIndex = -1;
    public InputControlScheme UIScheme
    {
        get
        {
            if (m_UISchemeIndex == -1) m_UISchemeIndex = asset.FindControlSchemeIndex("UI");
            return asset.controlSchemes[m_UISchemeIndex];
        }
    }
    public interface IUIActions
    {
        void OnAdvance(InputAction.CallbackContext context);
        void OnRetract(InputAction.CallbackContext context);
    }
}
