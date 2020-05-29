using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rysys.Particles
{
    public interface IParticleContainer
    {
        Particle[] List { get; set; }
        int Count { get; set; }
        int Capacity { get; }
        int Start { get; set; }
    }
    public class ParticleContainer : IParticleContainer
    {
        private int _start { get; set; }
        public Particle[] List { get; set; }
        public int Count { get; set; }
        public int Capacity { get => List.Length; }
        public int Start
        {
            get => _start;
            set => _start = value % List.Length;
        }
        public Particle this[int i]
        {
            get => List[(_start + i) % List.Length];
            set => List[(_start + i) % List.Length] = value;
        }

        public ParticleContainer(int capacity)
        {
            List = new Particle[capacity];
        }

        public static void Swap(ParticleContainer list, int index1, int index2)
        {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
}
