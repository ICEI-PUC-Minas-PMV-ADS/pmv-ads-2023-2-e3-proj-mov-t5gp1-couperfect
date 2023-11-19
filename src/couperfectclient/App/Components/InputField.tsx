import React, { useState } from 'react';
import { StyleSheet, TextInput } from 'react-native';
import colors from '../Styles';

const InputField: React.FC<{ placeholder: string; secureTextEntry?: boolean }> = ({ placeholder, secureTextEntry }) => {
  const [value, setValue] = useState('');

  return (
    <TextInput
      style={styles.input}
      onChangeText={setValue}
      placeholderTextColor={colors.NeutralContrast}
      value={value}
      placeholder={placeholder}
      secureTextEntry={secureTextEntry}
    />
  );
};

const styles = StyleSheet.create({
  input: {
    height: 40,
    borderColor: colors.PrimaryContrast,
    borderWidth: 1,
    marginBottom: 12,
    paddingLeft: 8,
    borderRadius: 4,
    color: colors.NeutralContrast
  },
});

export default InputField;