using BaseRPG.Controller.Input;
using BaseRPG.Controller.Interfaces;
using BaseRPG.Controller.UnitControl;
using BaseRPG.Controller.UnitControl.ItemCollection;
using BaseRPG.Model.Game;
using BaseRPG.Model.Interfaces.Movement;
using BaseRPG.Model.Skills;
using BaseRPG.Model.Tickable.FightingEntity.Enemy;
using BaseRPG.Model.Tickable.FightingEntity.Enemy.Factories;
using BaseRPG.Model.Tickable.FightingEntity.Hero;
using BaseRPG.Model.Tickable.Item;
using BaseRPG.Model.Tickable.Item.Factories.WeaponFactories;
using BaseRPG.Model.Tickable.Item.Weapon;
using BaseRPG.Model.Worlds.Blocks;
using BaseRPG.Model.Worlds.InteractionPoints;
using BaseRPG.Physics.TwoDimensional;
using BaseRPG.Physics.TwoDimensional.Collision;
using BaseRPG.Physics.TwoDimensional.Movement;
using BaseRPG.View.Animation.Utility;
using BaseRPG.View.Camera;
using BaseRPG.View.EntityView;
using BaseRPG.View.EntityView.Factory.UnitViewFactory;
using BaseRPG.View.Image;
using BaseRPG.View.Interfaces;
using BaseRPG.View.Interfaces.Factory;
using BaseRPG.View.Interfaces.Providers;
using BaseRPG.View.ItemView.Factory;
using SimpleInjector;
using System.Collections.Generic;

namespace BaseRPG.Controller.Initialization.GameConfiguring
{


    public class GameConfigurer : IGameConfigurer
    {
        private MainWindow mainWindow;
        private readonly IViewManager viewManager;
        private Controller controller;
        private Container container;
        public static readonly double VERY_LARGE_NUMBER = 9999999999999999999D;

        public GameConfigurer(MainWindow mainWindow, IViewManager viewManager, Controller controller)
        {
            this.mainWindow = mainWindow;
            this.viewManager = viewManager;
            this.controller = controller;
        }

        public IReadOnlyGameConfiguration Configure(Container container)
        {
            this.container = container;
            var config = createConfig();
            createUnits(config);
            afterHeroCreated(config);
            createWorldElements(config);
            return config;
        }
        private GameConfiguration createConfig()
        {
            var config = new GameConfiguration();

            config.InputHandler = container.GetInstance<InputHandler>();
            config.GlobalMousePositionObserver = new PositionObserver(() => config.InputHandler.MousePosition + mainWindow.CameraPosition);
            config.FollowingCamera = new FollowingCamera2D(new(0, 0),viewManager.Canvas.Size);
            config.DrawableProvider = container.GetInstance<IDrawableProvider>();
            
            config.ImageProvider = container.GetInstance<IImageProvider>();

            config.CollisionNotifier2D = new CollisionNotifier2D();
            config.CollisionNotifier2D.KeepTrackOf(config.GlobalMousePositionObserver);
            config.Binding = BindingHandler.CreateAndLoad(@"Assets\config\input-binding.json");
            config.Game = Game.CreateDefault(
                (name, world) =>
                    viewManager.SetCurrentWorldView(name, world, config.ImageProvider, config.FollowingCamera),
                config.CollisionNotifier2D,
                container.GetInstance<IPhysicsFactory>());
            config.AnimationProvider = AnimationProvider.CreateDefault();
            return config;
        }

        private void createUnits(GameConfiguration config)
        {
            createHero(config);

            createEnemy(config, 150, 300, @"Assets\image\enemies\zombie-outlined.png", config.AnimationProvider.Get("zombie-attack"), 0.2, 180);
            createEnemy(config, -200, 200, @"Assets\image\enemies\zombie-outlined.png", config.AnimationProvider.Get("zombie-attack"), 0.5, 50);
            createEnemy(config, -250, 0, @"Assets\image\enemies\slime-outlined.png", config.AnimationProvider.Get("slime-attack"), 0.1, 100);
            createEnemy(config, 150, -200, @"Assets\image\enemies\slime-outlined.png", config.AnimationProvider.Get("slime-attack"), 0.2, 0.1,99999);

        }
        private void afterHeroCreated(GameConfiguration config)
        {
            registerHeroDependentClasses(config.Hero);
            
            config.FollowingCamera.FollowedUnit = config.Hero;
            config.PlayerControl = new PlayerControl(config.Hero, config.DrawableProvider);
            config.PlayerControl.MousePositionProvider = config.InputHandler.MousePositionTracker;
            config.InventoryControl =  new InventoryControl(config.Hero.Inventory, config.DrawableProvider, controller);
            config.SpellControl = new SpellControl(config.Hero.SkillManager, config.DrawableProvider, controller.BoolCallbackQueue);
            config.ShopControl = new(config.PlayerControl);
        }
        private void registerHeroDependentClasses(Hero hero) {
            //container.Register<PlayerControl>(() =>
            //{
            //    var drawableProvider = container.GetInstance<IDrawableProvider>();
            //    return new PlayerControl(hero, drawableProvider);
            //});
            //container.Register<InventoryControl>(() => {
            //    var drawableProvider = container.GetInstance<IDrawableProvider>();
            //    var controller = container.GetInstance<Controller>();
            //    return new InventoryControl(hero.Inventory, drawableProvider,controller);
            //});
            //container.Register<SpellControl>(() => {
            //    var drawableProvider = container.GetInstance<IDrawableProvider>();
            //    var controller = container.GetInstance<Controller>();
            //    return new SpellControl(hero.SkillManager, drawableProvider, controller.BoolCallbackQueue);
            //});
        }
        private void createWorldElements(GameConfiguration config)
        {
            subscribeItemCreationEvents(config);
            createAndInitShops(config);
            createAndInitBlocks(config);
        }

        private void createAndInitBlocks(GameConfiguration config)
        {
            var block = new Block(new PositionUnit2D(200, 0), config.CurrentWorld);
            var pos = PositionUnit2D.ToVector2D(block.Position);
            var image = new DrawingImage(@"Assets\image\blocks\normal-block-outlined.png", config.ImageProvider, pos);
            config.AddDrawable(image);

            var movementManager = new PhysicsFactory2D().CreateMovementManager(block.Position);
            var shapeVertices = Polygon.RectangleVertices(new(0, 0), 128, 128);
            var shape = new Polygon(block, movementManager, shapeVertices);
            config.AddShape(shape);
        }

        private void createAndInitShops(GameConfiguration config)
        {
            var swordFactory = new SimpleSwordFactory();
            var bowFactory = new SimpleBowFactory();
            var wandFactory = new SimpleWandFactory();
            var daggerFactory = new SimpleDaggerFactory();

            var shop = createShop(config, 150, 200);
            shop.AddItem(swordFactory.Create(config.CurrentWorld), 1);
            shop.AddItem(bowFactory.Create(config.CurrentWorld), 2);

            var shop2 = createShop(config, 0, 200);
            //shop2.AddItem(swordFactory.Create(config.CurrentWorld), 1);
            shop2.AddItem(wandFactory.Create(config.CurrentWorld), 1);
            shop2.AddItem(daggerFactory.Create(config.CurrentWorld), 1);
        }
        private void subscribeItemCreationEvents(IReadOnlyGameConfiguration config)
        {
            SimpleSwordFactory.OnItemCreatedStatic += item =>
                connectViews(config, item, new SwordViewFactory(createCreationParams(config, item), config.InputHandler));
            SimpleBowFactory.OnItemCreatedStatic += item =>
                connectViews(config, item, new BowViewFactory(createCreationParams(config, item), config.AnimationProvider, config.InputHandler));
            SimpleWandFactory.OnItemCreatedStatic += item =>
                connectViews(config, item, new WandViewFactory(createCreationParams(config, item),viewManager, config.InputHandler));
            SimpleDaggerFactory.OnItemCreatedStatic += item =>
                connectViews(config, item, new DaggerViewFactory(createCreationParams(config, item)));
        }
        private void createHero(GameConfiguration config)
        {
            var heroMovementManager = config.PhysicsFactory.CreateMovementManager();
            config.Hero = new Hero.HeroBuilder(100, heroMovementManager, config.CurrentWorld).Build() as Hero;
            config.Hero.SkillManager = createSkillManagerForHero(config);
            var shape = createShapeForHero(config);
            var heroView = createHeroView(config, shape);
            config.DrawableProvider.Connect(heroView, config.Hero);
            config.AddDrawable(heroView);
            config.AddShape(shape);
        }
        private void createEnemy(GameConfiguration config, double x, double y, string idleImage, List<string> attackAnimation, double timeBetweenFrames, double speed, int hp = 100)
        {
            Enemy enemy = new SlimeEnemyFactory(config.PhysicsFactory, config.Hero, speed, hp)
                .Create(config.PhysicsFactory.CreatePosition(x, y));
            Polygon shape = Polygon.ForUnit(enemy, Polygon.CircleVertices(new(0, 0), 50), config.CollisionNotifier2D);

            SlimeViewCreationParams param = new SlimeViewCreationParams
            {
                Image = idleImage,
                Controller = controller,
                Enemy = enemy,
                ImageProvider = config.ImageProvider,
                Shape = shape,
                AttackAnimation = attackAnimation,
                TimeBetweenFrames = timeBetweenFrames,
            };
            UnitView enemyView = new SlimeViewFactory(param).Create();

            var enemyConfigurer = new EnemyConfigurer(enemy)
                .WithUnitView(enemyView)
                .Range(70);
            config.DrawableProvider.Connect(enemyView, enemy);
            config.AddDrawable(config.DrawableProvider.GetDrawablesAsListOf(enemy)[0], 100);
            config.AddShape(shape);
            config.AddShape(enemyConfigurer.FullInRangeDetectorShape);
        }

        private Shop createShop(GameConfiguration config, int x, int y)
        {
            var shop = new Shop(new PositionUnit2D(x, y), config.CurrentWorld);
            shop.InteractionStarted += (h, s) =>
            {
                config.ShopControl.ClickedOnShop(shop);
            };
            var shopPos = PositionUnit2D.ToVector2D(shop.Position);
            config.AddDrawable(new DrawingImage(@"Assets\image\blocks\shop-outlined.png", config.ImageProvider, shopPos));

            var movementManager = new PhysicsFactory2D().CreateMovementManager(shop.Position);
            var shapeVertices = Polygon.RectangleVertices(new(0, 0), 128, 128);
            var shape = new Polygon(shop, movementManager, shapeVertices);
            config.AddShape(shape);
            return shop;

        }
        private Dictionary<string, IDrawable> connectViews(IReadOnlyGameConfiguration config, Item weapon, IItemViewFactory factory)
        {
            var result = factory.Create();
            connect(config, result, weapon);
            return result;
        }
        private ItemViewCreationParams createCreationParams(IReadOnlyGameConfiguration config, Item item)
        {
            var weapon = item as Weapon;
            var viewCreationParams = new ItemViewCreationParams
            {
                Controller = controller,
                GlobalMousePositionObserver = config.GlobalMousePositionObserver,
                ImageProvider = config.ImageProvider,
                Weapon = weapon
            };
            return viewCreationParams;
        }
        private void connect(IReadOnlyGameConfiguration config, Dictionary<string, IDrawable> views, Item item)
        {
            config.DrawableProvider.Connect(views["equipped"], item, "equipped");
            config.DrawableProvider.Connect(views["inventory"], item, "inventory");
        }
        private SkillManager createSkillManagerForHero(GameConfiguration config)
        {
            var configurers = ISkillManagerConfigurer.CreateConfigurers(config.Hero, controller);
            var builder = new SkillManager.Builder();
            configurers.ForEach(s => s.Configure(builder, config));
            return builder.Create();
        }
        private IShape2D createShapeForHero(GameConfiguration config)
        {
            return Polygon.ForUnit(
                config.Hero,
                Polygon.RectangleVertices(new(0, 0), 120, 120),
                config.CollisionNotifier2D
                );
        }
        private UnitView createHeroView(GameConfiguration config, IShape2D shape)
        {
            var param = new HeroViewCreationParams
            {
                Hero = config.Hero,
                Shape = shape,
                ImageProvider = config.ImageProvider,
                HeroImage = @"Assets\image\characters\character1-outlined.png",
            };
            return new HeroViewFactory(param).Create();
        }
    }
}
