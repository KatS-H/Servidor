using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    internal class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();
            int _colorIndex = _packet.ReadInt();  // Leer el índice de color
            bool _isFemale = _packet.ReadBool(); // Leer el género

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}. ");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})");
            }
            Server.clients[_fromClient].SendIntoGame(_username, _colorIndex, _isFemale);
        }

        //******************* INICIO 1
        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            try
            {
                // Verificar que el paquete tenga suficientes datos
                if (_packet.UnreadLength() < sizeof(int) + (3 * sizeof(float)) + sizeof(float) * 4 + sizeof(int))
                {
                    Console.WriteLine($"Paquete mal formado recibido del cliente {_fromClient}.");
                    return;
                }

                // Leer los datos del paquete en el mismo orden en que se enviaron
                int _clientId = _packet.ReadInt(); // Leer el ID del jugador
                Vector3 _position = _packet.ReadVector3(); // Leer la posición
                Quaternion _rotation = _packet.ReadQuaternion(); // Leer la rotación
                string _animation = _packet.ReadString(); // Leer la animación

                // Actualizar el estado del jugador en el servidor
                Server.clients[_fromClient].player.SetPosition(_position);
                Server.clients[_fromClient].player.SetRotation(_rotation);
                Server.clients[_fromClient].player.SetAnimationState(_animation);

                // Reenviar el estado a todos los clientes (excepto al que lo envió)
                ServerSend.PlayerState(_fromClient, _position, _rotation, _animation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling player movement from client {_fromClient}: {ex}");
            }
        }
        //******************* FIN 1

    }
}