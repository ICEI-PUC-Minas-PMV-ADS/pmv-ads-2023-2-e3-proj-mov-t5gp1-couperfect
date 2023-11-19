import React, {useContext, useState} from 'react';
import {View} from 'react-native';
import Background from '../Components/Background';
import CoupButton from '../Components/CoupButton';
import IconButton from '../Components/IconButton';
import InputField from '../Components/InputField';
import ModalLogo from '../Components/ModalLogo';
import AuthContext from '../Context/auth';

const SignInPage: React.FC = () => {
  const {signIn} = useContext(AuthContext);

  const handleSignIn = async () => await signIn({ email, password });

  const [isPasswordVisible, setPasswordVisibility] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  return (
    <Background>
      <ModalLogo>
        <View style={{flexDirection: 'column', alignItems: 'stretch'}}>
          <InputField
            placeholder="Email"
            style={{width: '100%'}}
            onTextChange={setEmail}
          />
          <View style={{flexDirection: 'row', width: '100%'}}>
            <InputField
              placeholder="Senha"
              secureTextEntry={isPasswordVisible}
              style={{width: '85%'}}
              onTextChange={setPassword}
            />
            <IconButton
              type="eye"
              isEyeOpen={isPasswordVisible}
              setEyeOpen={setPasswordVisibility}
            />
          </View>
          <CoupButton onPress={handleSignIn} title="Entrar" />
        </View>
      </ModalLogo>
    </Background>
  );
};

export default SignInPage;
