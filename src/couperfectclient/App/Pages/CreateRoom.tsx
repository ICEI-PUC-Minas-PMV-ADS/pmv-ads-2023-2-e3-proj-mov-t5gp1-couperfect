import {useState} from 'react';
import Background from '../Components/Background';
import CoupButton from '../Components/CoupButton';
import InputField from '../Components/InputField';
import ModalLogo from '../Components/ModalLogo';
import PasswordField from '../Components/PasswordField';

const CreateRoom: React.FC = () => {
  const [roomName, setRoomName] = useState('');
  const [roomPassword, setRoomPassword] = useState('');

  function handleCreateRoom() {
    console.log(`criando a sala ${roomName}`);
    if (roomPassword !== '') console.log(`com o a senha ${roomPassword}`)
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
