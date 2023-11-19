import {View} from 'react-native';
import InputField from './InputField';
import IconButton from './IconButton';
import React, { useState } from 'react';

interface PasswordFieldProps {
    placeholder: string;
    onTextChange(e: string) : void;
}

const PasswordField: React.FC<PasswordFieldProps> = ({ onTextChange, placeholder }) => {

    const [isPasswordVisible, setPasswordVisibility] = useState(false);

  return (
    <View style={{flexDirection: 'row', width: '100%'}}>
      <InputField
        placeholder={placeholder}
        secureTextEntry={!isPasswordVisible}
        style={{width: '85%'}}
        onTextChange={onTextChange}
      />
      <IconButton
        type="eye"
        isEyeOpen={isPasswordVisible}
        setEyeOpen={setPasswordVisibility}
      />
    </View>
  );
};

export default PasswordField;