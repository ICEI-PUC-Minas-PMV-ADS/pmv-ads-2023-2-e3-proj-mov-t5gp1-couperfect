import React, { useContext, useState } from 'react';
import { ImageBackground, StyleSheet, View } from 'react-native';
import InputField from '../Components/InputField';
import CoupButton from '../Components/CoupButton';
import AuthContext from '../Context/auth';
import ModalLogo from '../Components/ModalLogo';
import { useNavigation } from '@react-navigation/native';
import { CommonActions } from '@react-navigation/native';
import IconButton from '../Components/IconButton';

const SignInPage: React.FC = () => {
  const { signed, signIn, user } = useContext(AuthContext);
  const navigation = useNavigation();

  console.log(signed);
  console.log(user);

  const handleSignIn = async () => {
    navigation.dispatch(CommonActions.navigate('SignUpPage'));
  };

  const [isPasswordVisible, setPasswordVisibility] = useState(false);

  return (
    <ImageBackground
      source={require('../Assets/Background.png')}
      style={styles.background}>
      <ModalLogo>
        <View style={{ flexDirection: 'column', alignItems: 'stretch' }}>
          <InputField placeholder="Email" style={{ width: '100%' }} />
          <View style={{ flexDirection: 'row', width: '100%' }}>
            <InputField placeholder="Senha" secureTextEntry={isPasswordVisible} style={{ width: '85%' }} />
            <IconButton type='eye' isEyeOpen={isPasswordVisible} setEyeOpen={setPasswordVisibility} />
          </View>
          <CoupButton onPress={handleSignIn} title="Entrar" />
        </View>
      </ModalLogo>
    </ImageBackground>
  );
};

const styles = StyleSheet.create({
  background: {
    width: '100%',
    height: '100%',
    justifyContent: 'center',
    alignItems: 'center'
  },
  container: {

  }
});

export default SignInPage;
