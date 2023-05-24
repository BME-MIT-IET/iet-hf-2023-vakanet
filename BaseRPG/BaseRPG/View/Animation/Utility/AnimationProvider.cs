using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.View.Animation.Utility
{
    public class AnimationProvider
    {
        private Dictionary<string, List<string>> animations = new();

        public AnimationProvider(Dictionary<string, List<string>> animations)
        {
            this.animations = animations;
        }

        public List<string> Get(string key)
        {
            return animations[key];
        }

        public static AnimationProvider CreateDefault()
        {
            var mapping = new Dictionary<string, List<string>>();

            mapping.Add("slime-attack",
                new List<string>{
                    @"Assets\image\enemies\slime-attack-animation\slime-attack-0-outlined.png",
                    @"Assets\image\enemies\slime-attack-animation\slime-attack-1-outlined.png",
                    @"Assets\image\enemies\slime-attack-animation\slime-attack-2-outlined.png",
                    @"Assets\image\enemies\slime-attack-animation\slime-attack-3-outlined.png",
                }
            );
            mapping.Add("zombie-attack",
                new List<string>{
                    @"Assets\image\enemies\zombie-attack-animation\zombie-attack-1-outlined.png",
                    @"Assets\image\enemies\zombie-attack-animation\zombie-attack-2-outlined.png",
                }
            );
            mapping.Add("meteor",
                new List<string> {
                    @"Assets\image\effects\meteor\meteor-0.png",
                    @"Assets\image\effects\meteor\meteor-1.png",
                    @"Assets\image\effects\meteor\meteor-2.png",
                    @"Assets\image\effects\meteor\meteor-3.png",
                    @"Assets\image\effects\meteor\meteor-4.png",
                    @"Assets\image\effects\meteor\meteor-5.png",
                    @"Assets\image\effects\meteor\meteor-6.png",
                }
            );
            mapping.Add("invincibility-effect",
                new List<string>{
                    @"Assets\image\effects\invincibility\invincibility-0.png",
                    @"Assets\image\effects\invincibility\invincibility-1.png"
                }
            );
            mapping.Add("dash-effect",
                new List<string>{
                    @"Assets\image\effects\dash\dash-00.png",
                    @"Assets\image\effects\dash\dash-01.png",
                    @"Assets\image\effects\dash\dash-02.png",
                    @"Assets\image\effects\dash\dash-03.png",
                    @"Assets\image\effects\dash\dash-04.png",
                    @"Assets\image\effects\dash\dash-05.png",
                    @"Assets\image\effects\dash\dash-06.png",
                    @"Assets\image\effects\dash\dash-07.png",
                    @"Assets\image\effects\dash\dash-08.png",
                    @"Assets\image\effects\dash\dash-09.png",
                }
            );
            mapping.Add("bow-attack",
                new List<string>{
                    @"Assets\image\weapons\normal-bow\attack\bow-attack-1-outlined.png",
                    @"Assets\image\weapons\normal-bow\attack\bow-attack-2-outlined.png",
                    @"Assets\image\weapons\normal-bow\attack\bow-attack-3-outlined.png",
                    @"Assets\image\weapons\normal-bow\attack\bow-attack-4-outlined.png",
                    @"Assets\image\weapons\normal-bow\attack\bow-attack-5-outlined.png",
                }
            );
            mapping.Add("sword-heavy-attack",
                new List<string>{
                    @"Assets\image\attacks\sword-heavy-attack-effect\sword-heavy-attack-0.png",
                    @"Assets\image\attacks\sword-heavy-attack-effect\sword-heavy-attack-1.png",
                    @"Assets\image\attacks\sword-heavy-attack-effect\sword-heavy-attack-2.png",
                    @"Assets\image\attacks\sword-heavy-attack-effect\sword-heavy-attack-3.png",
                    @"Assets\image\attacks\sword-heavy-attack-effect\sword-heavy-attack-4.png",
                    @"Assets\image\attacks\sword-heavy-attack-effect\sword-heavy-attack-5.png",
                }
            );
            mapping.Add("bow-heavy-attack-release",
                new List<string>{
                    @"Assets\image\weapons\normal-bow\heavy-attack-release\heavy-release-0-outlined.png",
                    @"Assets\image\weapons\normal-bow\heavy-attack-release\heavy-release-1-outlined.png",
                    @"Assets\image\weapons\normal-bow\heavy-attack-release\heavy-release-2-outlined.png",
                    @"Assets\image\weapons\normal-bow\heavy-attack-release\heavy-release-3-outlined.png",
                    @"Assets\image\weapons\normal-bow\heavy-attack-release\heavy-release-4-outlined.png",
                    @"Assets\image\weapons\normal-bow\heavy-attack-release\heavy-release-5-outlined.png",
                    @"Assets\image\weapons\normal-bow\heavy-attack-release\heavy-release-6-outlined.png",
                    @"Assets\image\weapons\normal-bow\heavy-attack-release\heavy-release-7-outlined.png",
                    @"Assets\image\weapons\normal-bow\heavy-attack-release\heavy-release-8-outlined.png",
                }
            );
            return new AnimationProvider(mapping);
        }
    }
}
