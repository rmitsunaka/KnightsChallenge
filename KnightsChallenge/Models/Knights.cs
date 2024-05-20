using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;
using System.Linq;

namespace KnightsChallenge.Models
{
    public class Knight
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 


        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("nickname")]
        public string Nickname { get; set; }

        [BsonElement("birthday")]
        public DateTime Birthday { get; set; }

        [BsonElement("weapons")]
        public List<Weapon> Weapons { get; set; }

        [BsonElement("attributes")]
        public Attributes Attributes { get; set; }

        [BsonElement("keyAttribute")]
        public string KeyAttribute { get; set; }

        [BsonElement("isHero")]
        public bool IsHero { get; set; }

        public int Age => (int)((DateTime.Now - Birthday).TotalDays / 365.25);

        public int Attack
        {
            get
            {
                var keyAttrMod = CalculateAttributeModifier(Attributes.GetValue(KeyAttribute));
                var weaponMod = Weapons.Sum(w => w.Mod);
                return 10 + keyAttrMod + weaponMod;
            }
        }

        public int Experience => Age >= 7 ? (int)((Age - 7) * Math.Pow(22, 1.45)) : 0;

        private int CalculateAttributeModifier(int value)
        {
            if (value <= 8) return -2;
            if (value <= 10) return -1;
            if (value <= 12) return 0;
            if (value <= 15) return 1;
            if (value <= 18) return 2;
            return 3;
        }
    }

    public class Weapon
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("mod")]
        public int Mod { get; set; }
    }

    public class Attributes
    {
        [BsonElement("strength")]
        public int Strength { get; set; }

        [BsonElement("dexterity")]
        public int Dexterity { get; set; }

        [BsonElement("constitution")]
        public int Constitution { get; set; }

        [BsonElement("intelligence")]
        public int Intelligence { get; set; }

        [BsonElement("wisdom")]
        public int Wisdom { get; set; }

        [BsonElement("charisma")]
        public int Charisma { get; set; }

        public int GetValue(string attribute)
        {
            switch (attribute.ToLower())
            {
                case "strength":
                    return Strength;
                case "dexterity":
                    return Dexterity;
                case "constitution":
                    return Constitution;
                case "intelligence":
                    return Intelligence;
                case "wisdom":
                    return Wisdom;
                case "charisma":
                    return Charisma;
                default:
                    return 0;
            }
        }
    }
}
