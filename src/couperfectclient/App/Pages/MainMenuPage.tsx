import React, { useContext } from 'react';
import { Image, Pressable, StyleSheet } from 'react-native';
import Anchor from '../Components/Anchor';
import Background from '../Components/Background';
import ListItem from '../Components/ListItem';
import AuthContext from '../Context/auth';

const MainMenuPage: React.FC = () => {
  const {signOut} = useContext(AuthContext);

  return (
    <Background style={styles.background}>
      <Image style={styles.logo} source={require('../Assets/Logo.png')} />
      <Anchor route="QueryRooms" text="Buscar Salas" />
      <Anchor route="CreateRoom" text="Criar Sala" />
      <Pressable onPress={signOut}>
        <ListItem text="Sair" />
      </Pressable>
    </Background>
  );
};
const styles = StyleSheet.create({
  background: {
    justifyContent: 'flex-end',
    alignItems: 'flex-start'
  },
  logo: {
    position: 'relative',
    left: 420,
    top: 100,
    width: 260,
    height: 130,
    resizeMode: 'contain',
  },
  textStyle: {
    color: 'white',
    paddingBottom: 5,
    paddingRight: 30,
    paddingLeft: 40,
    fontSize: 30,
  },
});

export default MainMenuPage;
