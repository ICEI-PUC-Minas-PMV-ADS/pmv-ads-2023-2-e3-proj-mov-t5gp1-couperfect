import React, {useContext} from 'react';
import {ImageBackground, StyleSheet, View} from 'react-native';
import InputField from '../Components/InputField';
import CoupButton from '../Components/CoupButton';
import AuthContext from '../Context/auth';
import ModalLogo from '../Components/ModalLogo';

const SignInPage: React.FC = () => {
  const {signed, signIn, user} = useContext(AuthContext);
  console.log(signed);
  console.log(user);

  const handleSignIn = async () => {
    await signIn();
  };

  return (
    <ImageBackground
      source={require('../Assets/Background.png')}
      style={styles.background}>
      <ModalLogo>
        
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
  container : {

  }
});

export default SignInPage;
