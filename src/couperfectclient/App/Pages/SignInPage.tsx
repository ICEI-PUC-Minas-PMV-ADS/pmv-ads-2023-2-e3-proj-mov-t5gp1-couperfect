import React, { useContext, useState } from 'react';
import Background from '../Components/Background';
import CoupButton from '../Components/CoupButton';
import InputField from '../Components/InputField';
import ModalLogo from '../Components/ModalLogo';
import PasswordField from '../Components/PasswordField';
import AuthContext from '../Context/auth';

const SignInPage: React.FC = () => {
  const {signIn} = useContext(AuthContext);

  const handleSignIn = async () => await signIn({ email, password });

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  return (
    <Background>
      <ModalLogo title='Login'>
          <InputField
            placeholder="Email"
            style={{width: '100%'}}
            onTextChange={setEmail}
          />
          <PasswordField onTextChange={setPassword} placeholder='Senha'/>
          <CoupButton onPress={handleSignIn} title="Entrar" />
      </ModalLogo>
    </Background>
  );
};

export default SignInPage;
