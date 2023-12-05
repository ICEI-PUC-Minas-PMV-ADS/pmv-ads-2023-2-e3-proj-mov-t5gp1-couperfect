import {useState} from 'react';
import Background from '../Components/Background';
import CoupButton from '../Components/CoupButton';
import InputField from '../Components/InputField';
import ModalLogo from '../Components/ModalLogo';
import PasswordField from '../Components/PasswordField';
import axios from 'axios';

const CreateRoom: React.FC = () => {
  const [roomName, setRoomName] = useState('');
  const [roomPassword, setRoomPassword] = useState('');

  async function handleCreateRoom() {
    const handleCreateRoom = async () => {
      try {
        const response = await axios.post('http://localhost:3000/gameRooms', {
          name: roomName,
          password: roomPassword,
          players: []
        });
    
        if (response.status === 201) {
          console.log(`Sala ${roomName} criada com sucesso!`);
          if (roomPassword !== '') console.log(`com a senha ${roomPassword}`);
        } else {
          console.error('Erro ao criar a sala:', response.statusText);
        }
      } catch (error) {
        console.error('Erro ao criar a sala:', error);
      }
    }
  }

  return (
    <Background>
      <ModalLogo title="Criar sala">
        <InputField
          placeholder="Nome da sala"
          style={{width: '100%'}}
          onTextChange={setRoomName}
        />
        <PasswordField
          onTextChange={setRoomPassword}
          placeholder="Senha da sala (opcional)"
        />
        <CoupButton title="Criar" onPress={handleCreateRoom} />
      </ModalLogo>
    </Background>
  );
};

export default CreateRoom;
