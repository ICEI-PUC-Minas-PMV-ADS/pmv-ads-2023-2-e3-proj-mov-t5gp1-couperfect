import React from 'react';
import { StyleSheet, Image, ImageBackground, Pressable } from 'react-native';
import { useNavigation, CommonActions } from '@react-navigation/native';

const SplashPage: React.FC = () => {
  const navigation = useNavigation();

  const handlePress = () => {
    navigation.dispatch(CommonActions.navigate('HomePage'));
  };

  return (
    <Pressable style={styles.container} onPress={handlePress}>
      {() => (
        <ImageBackground source={require('../Assets/Background.png')} style={styles.backgroundImage}>
          <Pressable onPress={handlePress} style={styles.logoContainer}>
            <Image source={require('../Assets/Logo.png')} style={styles.logo} />
          </Pressable>
        </ImageBackground>
      )}
    </Pressable>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  backgroundImage: {
    flex: 1,
    width: '100%',
    height: '100%'
  },
  logoContainer: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  logo: {
    width: 200,
    height: 200,
    resizeMode: 'contain',
  }
});

export default SplashPage;
