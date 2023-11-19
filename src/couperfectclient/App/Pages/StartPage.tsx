import React from 'react';
import { View,StyleSheet, Image, ImageBackground, TouchableOpacity } from 'react-native';
import TextButton from '../Components/TextButton';
import { useNavigation  } from '@react-navigation/native';
import AppRoutes from '../Routes/app.routes';

const StartPage:React.FC = () => {

  const navigation = useNavigation<any>();
  
  const handleLogoPress = () => {
    navigation.navigate('SignInPage');
  };

  return (
    <View style={styles.container}>
      <ImageBackground source={require('../Assets/Background.png')} style={styles.backgroundImage} />
      <TouchableOpacity onPress={handleLogoPress}>
        <Image source={require('../Assets/Logo.png')} style = {styles.logo} />
      </TouchableOpacity>
      <AppRoutes/>
        <TextButton title="Entrar" onPress={() => navigation.navigate('SignInPage')} />
        <TextButton title="Registrar-se" onPress={() => navigation.navigate('Registrar-se')} />
        <TextButton title="Tutorial" onPress={() => navigation.navigate('Tutorial')} />
        <TextButton title="Sair" onPress={() => navigation.goBack('Sair')} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'flex-start',
    justifyContent: 'flex-end',
    padding: 20
  },
  backgroundImage: {
    position: 'absolute',
    top: 0,
    left: 0,
    bottom: 0,
    right: 0,
    transform: [{ rotate: '180 deg' }],
  },
  logo: {
    position:'relative',
    left:420,
    top:100,
    width: 200,
    height: 200,
    resizeMode: 'contain'
  }
});

export default StartPage;