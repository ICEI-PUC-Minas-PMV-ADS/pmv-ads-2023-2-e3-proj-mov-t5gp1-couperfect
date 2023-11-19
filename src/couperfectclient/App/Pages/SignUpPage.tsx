import React, { useContext } from 'react';
import { ImageBackground, StyleSheet } from 'react-native';
import ModalLogo from '../Components/ModalLogo';
import AuthContext from '../Context/auth';

const SignInPage: React.FC = () => {
  const {signed, signIn, user} = useContext(AuthContext);
  console.log(signed);
  console.log(user);

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
