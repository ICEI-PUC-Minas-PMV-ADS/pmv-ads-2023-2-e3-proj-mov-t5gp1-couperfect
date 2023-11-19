import React, { useState } from 'react';
import Background from '../Components/Background';
import CoupButton from '../Components/CoupButton';
import InputField from '../Components/InputField';
import ModalLogo from '../Components/ModalLogo';
import PasswordField from '../Components/PasswordField';

const SignUpPage: React.FC = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  function handleSignUp() {
    if (confirmPassword !== password) console.log('Senhas não coincidem');
    else console.log('Cadastrado com sucesso!');
  }

  return (
    <Background>
      <ModalLogo>
          <InputField
            placeholder="Nome"
            style={{width: '100%'}}
            onTextChange={setName}
          />
          <InputField
            placeholder="Email"
            style={{width: '100%'}}
            onTextChange={setEmail}
          />
          <PasswordField onTextChange={setPassword} placeholder="Senha" />
          <PasswordField onTextChange={setConfirmPassword} placeholder="Confirmar Senha" />
          <CoupButton onPress={handleSignUp} title="Registrar-se" />
      </ModalLogo>
    </Background>
  );
};

export default SignUpPage;
