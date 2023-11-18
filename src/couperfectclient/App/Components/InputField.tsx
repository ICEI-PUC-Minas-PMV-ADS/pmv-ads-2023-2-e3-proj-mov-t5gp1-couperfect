import React, { useState } from 'react';
import { StyleSheet, TextInput, TextStyle } from 'react-native';
import colors from '../Styles';

interface InputFieldProp {
  placeholder: string;
  secureTextEntry?: boolean;
  style?: TextStyle
}

const InputField: React.FC<InputFieldProp> = ({ placeholder, secureTextEntry, style }) => {
  const [value, setValue] = useState('');

  const styles = StyleSheet.compose(defaultStyle, style);

  return (
    <TextInput
      style={styles}
      onChangeText={setValue}
      value={value}
      placeholder={placeholder}
      secureTextEntry={secureTextEntry}
    />
  );
};

const defaultStyle = {
  height: 40,
  borderColor: colors.PrimaryContrast,
  borderWidth: 1,
  marginBottom: 12,
  paddingLeft: 8,
  borderRadius: 4,
  color: colors.NeutralContrast
};

export default InputField;