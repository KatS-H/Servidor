using System.Net.Quic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    internal class Player
    {
        public int id;
        public string username;

        // Estado del jugador
        public Vector3 position; // Posición del jugador
        public Quaternion rotation; // Rotación del jugador
        public string animationState; // Estado de la animación
        public bool isJumping; // Indica si el jugador está saltando
        public bool isFalling; // Indica si el jugador está cayendo

        // Selección del personaje
        public int colorIndex;  // Índice de color del personaje
        public bool isFemale;   // Género del personaje

        public Player(int _id, string _username, Vector3 _spawnPosition, int _colorIndex, bool _isFemale)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

            // Inicializar la selección del personaje
            colorIndex = _colorIndex;
            isFemale = _isFemale;

        }

        //******************* INICIO 1
        // Métodos para actualizar el estado del jugador
        public void SetPosition(Vector3 _position)
        {
            position = _position;
        }

        public void SetRotation(Quaternion _rotation)
        {
            rotation = _rotation;
        }

        public void SetAnimationState(string _animationState)
        {
            animationState = _animationState;
        }

        public void SetIsJumping(bool _isJumping)
        {
            isJumping = _isJumping;
        }

        public void SetIsFalling(bool _isFalling)
        {
            isFalling = _isFalling;
        }
        //******************* FIN 1
    }
}