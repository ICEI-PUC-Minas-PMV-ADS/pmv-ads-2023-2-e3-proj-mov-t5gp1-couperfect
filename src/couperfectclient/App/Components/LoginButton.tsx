import React from 'react';
import { Button } from 'react-native';

const LoginButton: React.FC<{ onPress: () => void }> = ({ onPress }) => {
  return <Button onPress={onPress} title="Entrar" />;
};

export default LoginButton;