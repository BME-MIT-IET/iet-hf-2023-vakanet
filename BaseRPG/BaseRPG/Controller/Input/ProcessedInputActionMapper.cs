using BaseRPG.Controller.Initialization.GameConfiguring;
using BaseRPG.Controller.Input.InputActions;
using BaseRPG.Controller.Input.InputActions.Attack;
using BaseRPG.Controller.Input.InputActions.Effect;
using BaseRPG.Controller.Input.InputActions.Interaction;
using BaseRPG.Controller.Input.InputActions.Movement;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.View.Interfaces;
using BaseRPG.View.UIElements;
using BaseRPG.View.UIElements.ItemCollectionUI;
using BaseRPG.View.UIElements.Spell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Input
{
    public class ProcessedInputActionMapper : IProcessedInputActionMapper
    {
        private Dictionary<string, IInputAction> inputActionMap;
        private ProcessedInputActionMapper(Dictionary<string, IInputAction> inputActionMap)
        {
            this.inputActionMap = inputActionMap;
        }

        public Dictionary<string, Action> InputActionMap { get; set; }

        public IEnumerable<IInputAction> ToAction(IEnumerable<string> keys) {
            var result = new LinkedList<IInputAction>();
            foreach (var key in keys) {
                if (inputActionMap.ContainsKey(key))
                    result.AddLast(inputActionMap[key]);
            }
            return result;
        }
        public static ProcessedInputActionMapper Default(IReadOnlyGameConfiguration config,MainWindow window)
        {
            var playerControl = config.PlayerControl;
            return new Builder().AddMapping("move-forward", new MovementInputAction(MoveDirection.Forward, playerControl))
                .AddMapping("move-left", new MovementInputAction(MoveDirection.Left, playerControl))
                .AddMapping("move-right", new MovementInputAction(MoveDirection.Right, playerControl))
                .AddMapping("move-backward", new MovementInputAction(MoveDirection.Backward, playerControl))
                .AddMapping("light-attack", new LightAttackInputAction(playerControl))
                .AddMapping("heavy-attack", new HeavyAttackInputAction(playerControl))
                .AddMapping("skill-1", new DashSkillOnPressInputAction(playerControl.ControlledUnit, config.GlobalMousePositionObserver, 0))
                .AddMapping("skill-2", new InvincibilitySkillOnPressInputAction(playerControl.ControlledUnit, 1))
                .AddMapping("skill-3", new MeteorCreatingSkillOnReleaseInputAction(
                    playerControl.ControlledUnit,
                    config.GlobalMousePositionObserver,
                    window.Controller,
                    config.ImageProvider,
                    config.AnimationProvider
                    ))
                .AddMapping("skill-4", new DamagingStunSkillOnPressInputAction(playerControl.ControlledUnit, config.CollisionNotifier2D))
                .AddMapping("settings-window", new CustomInputAcion(actionOnPressed: () => window.WindowControl.OpenOrClose(SettingsWindow.WindowName)))
                .AddMapping("inventory-window", new CustomInputAcion(actionOnPressed: () => window.WindowControl.OpenOrClose(InventoryWindow.WindowName)))
                .AddMapping("spells-window", new CustomInputAcion(actionOnPressed: () => window.WindowControl.OpenOrClose(SpellsWindow.WindowName)))
                .AddMapping("initiate-interaction", new TargetClickInputAction(config.CollisionNotifier2D, playerControl.ControlledUnitAsHero))
                .Create();
        }
        public class Builder{
            private Dictionary<string, IInputAction> inputActionMap = new();
            public Builder AddMapping(string name, IInputAction action) {
                inputActionMap.Add(name, action);
                return this;
            }
            public ProcessedInputActionMapper Create() {
                return new ProcessedInputActionMapper(inputActionMap);
            }
        }
    }
}
