import React, {useContext} from 'react';
import {Image, ImageBackground, Pressable, StyleSheet} from 'react-native';
import Anchor from '../Components/Anchor';
import ListItem from '../Components/ListItem';
import AuthContext from '../Context/auth';

const MainMenuPage: React.FC = () => {
  const {signOut} = useContext(AuthContext);

  return (
    <ImageBackground
      source={require('../Assets/Background.png')}
      style={styles.background}>
      <Image style={styles.logo} source={require('../Assets/Logo.png')} />
      <Anchor route="QueryRooms" text="Buscar Salas" />
      <Anchor route="CreateRoom" text="Criar Sala" />
      <Pressable onPress={signOut}>
        <ListItem text="Sair" />
      </Pressable>
    </ImageBackground>
  );
};
const styles = StyleSheet.create({
  background: {
    width: '100%',
    height: '100%',
    justifyContent: 'center',
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
