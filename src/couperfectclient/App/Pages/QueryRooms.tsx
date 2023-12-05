import React, {useState} from 'react';
import Background from '../Components/Background';
import ModalLogo from '../Components/ModalLogo';
import InputField from '../Components/InputField';
import CoupButton from '../Components/CoupButton';
import axios from 'axios';

const QueryRoomsPage: React.FC = () => {
  const [roomName, setRoomName] = useState('');

  function handleQueryRoom() {
    axios.get('http://localhost:3000/gameRooms')
      .then(response => {
        const room = response.data.find((room: { name: string }) => room.name === roomName);
        console.log(room);
      })
      .catch(error => console.error('Error:', error));
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
