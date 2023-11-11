import React, {useContext} from 'react';
import {StyleSheet, View} from 'react-native';
import InputField from '../Components/InputField';
import LoginButton from '../Components/LoginButton';
import AuthContext from '../Context/auth';

const SignInPage: React.FC = () => {
  const {signed, signIn, user} = useContext(AuthContext);
  console.log(signed);
  console.log(user);

  const handleSignIn = async () => {
    await signIn();
  };

  return (
    <View style={styles.container}>
      <InputField placeholder="Email" />
      <InputField placeholder="Senha" secureTextEntry />
      <LoginButton onPress={handleSignIn} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    padding: 16,
  },
});

export default SignInPage;
