using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour {


        /// <summary>
        /// The character's actions
        /// </summary>
		public enum Action{Left, LeftUp, Up, RightUp, Right, RightDown, Down, LeftDown, Power1, Power2, Power3, Power4};

        /// <summary>
        /// The character's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The character's current level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The character's current stats power to use
        /// </summary>
        public int UnusedPowerStats{ get; set; }

        /// <summary>
        /// The character's current level power 1
        /// </summary>
        public int Power1Stats { get; set; }

        /// <summary>
        /// The character's current level power 2
        /// </summary>
        public int Power2Stats { get; set; }

        /// <summary>
        /// The character's current level power 3
        /// </summary>
        public int Power3Stats { get; set; }

        /// <summary>
        /// The character's current level power 4
        /// </summary>
        public int Power4Stats { get; set; }


        /// <summary>
        /// The character's move left
        /// </summary>
        public virtual void Left(){

        }
        /// <summary>
        /// The character's move left up
        /// </summary>
        public virtual void LeftUp(){

        }
        /// <summary>
        /// The character's move up
        /// </summary>
        public virtual void Up(){

        }
        /// <summary>
        /// The character's move right up
        /// </summary>
        public virtual void RightUp(){

        }
        /// <summary>
        /// The character's move right
        /// </summary>
        public virtual void Right(){

        }
        /// <summary>
        /// The character's move right down
        /// </summary>
        public virtual void RightDown(){

        }
        /// <summary>
        /// The character's move down
        /// </summary>
        public virtual void Down(){

        }

        /// <summary>
        /// The character's move left down
        /// </summary>
        public virtual void LeftDown(){

        }

        /// <summary>
        /// The character's power 1
        /// </summary>
	public virtual void ButtonZ(){

        }

        /// <summary>
        /// The character's power 2
        /// </summary>
	public virtual void ButtonX(){

        }

        /// <summary>
        /// The character's power 3
        /// </summary>
	public virtual void ButtonC(){

        }

        /// <summary>
        /// The character's power 4
        /// </summary>
	public virtual void ButtonV(){

        }





}
