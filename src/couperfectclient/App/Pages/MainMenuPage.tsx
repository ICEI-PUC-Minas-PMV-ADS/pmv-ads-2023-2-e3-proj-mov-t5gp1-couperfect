import React from 'react';
import { Button, ImageBackground } from 'react-native';
import ModalLogo from '../Components/ModalLogo';

const MainMenuPage: React.FC = () => {
  return (
    <ImageBackground source={require('../Assets/Background.png')}>
      <ModalLogo>
        <Button title="fjdslfdad"/>
      </ModalLogo>
    </ImageBackground>
  );
};

export default MainMenuPage;