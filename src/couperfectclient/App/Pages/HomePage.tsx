import React from 'react';
import {
  BackHandler,
  Image,
  Platform,
  Pressable,
  StyleSheet
} from 'react-native';
import Anchor from '../Components/Anchor';
import Background from '../Components/Background';
import ListItem from '../Components/ListItem';

const HomePage: React.FC = () => {

  const exit = () => {
    if (Platform.OS === 'android') {
      if (BackHandler) {
        BackHandler.exitApp();
      }
    }
  };

  return (
    <Background style={styles.container}>
          <Image source={require('../Assets/Logo.png')} style={styles.logo} />

        <Anchor text="Entrar" route="SignInPage" />
        <Anchor text="Registrar-se" route="SingUpPage" />
        <Pressable onPress={exit}>
          <ListItem text="Sair" />
        </Pressable>
    </Background>
  );
};

const styles = StyleSheet.create({
  container: {
    alignItems: 'flex-start',
    justifyContent: 'flex-end'
  },
  logo: {
    position: 'relative',
    left: 420,
    top: 100,
    width: 200,
    height: 200,
    resizeMode: 'contain',
  },
});

export default HomePage;
