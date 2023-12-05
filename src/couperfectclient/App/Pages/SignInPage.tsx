import React, { useContext, useState } from 'react';
import Background from '../Components/Background';
import CoupButton from '../Components/CoupButton';
import InputField from '../Components/InputField';
import ModalLogo from '../Components/ModalLogo';
import PasswordField from '../Components/PasswordField';
import AuthContext from '../Context/auth';
import axios from 'axios';

const SignInPage: React.FC = () => {

  const {signIn} = useContext(AuthContext);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSignIn = async () => {
    try {
      const response = await axios.post('http://localhost:3000/players/login', {
        email,
        password
      });
  
      if (response.status === 200) {
        console.log('Login bem-sucedido!');
        // Faça algo com os dados de resposta (por exemplo, armazene o token de acesso)
      } else {
        console.error('Erro ao fazer login:', response.statusText);
      }
    } catch (error) {
      console.error('Erro ao fazer login:', error);
    }
  }

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
