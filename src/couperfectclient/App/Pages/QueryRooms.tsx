import React, {useState} from 'react';
import Background from '../Components/Background';
import ModalLogo from '../Components/ModalLogo';
import InputField from '../Components/InputField';
import CoupButton from '../Components/CoupButton';

const QueryRoomsPage: React.FC = () => {
  const [roomName, setRoomName] = useState('');

    function handleQueryRoom() {
        console.log(`buscando a sala: ${roomName}`);
    }

  return (
    <Background>
      <ModalLogo title="Buscar salas">
        <InputField
          placeholder="Nome da sala"
          style={{width: '100%'}}
          onTextChange={setRoomName}
        />
        <CoupButton title="Buscar" onPress={handleQueryRoom}/>
      </ModalLogo>
    </Background>
  );
};

export default QueryRoomsPage;
