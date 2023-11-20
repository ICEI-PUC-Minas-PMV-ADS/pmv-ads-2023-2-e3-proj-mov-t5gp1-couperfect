import { useNavigation } from '@react-navigation/native';
import { ReactNode } from 'react';
import { StyleSheet, Text, View } from 'react-native';
import CoupLogoSvg from '../Assets/Logo.svg';
import { colors, contrastTextStyle } from '../Styles';
import IconButton from './IconButton';

interface ModalLogoProps {
  children: ReactNode;
  title: string;
}

const ModalLogo: React.FC<ModalLogoProps> = ({children, title}) => {
  const navigate = useNavigation();

  return (
    <View style={styles.container}>
      <IconButton
        style={styles.iconButtonPosition}
        type="back"
        onPress={() => navigate.goBack()}
      />
      <View
        style={{
          alignItems: 'center',
          width: '50%',
        }}>
        <CoupLogoSvg />
      </View>
      <View style={styles.verticleLine} />
      <View style={{width: '50%', padding: 30}}>
        <View
          style={{
            width: '100%',
            justifyContent: 'center',
            alignItems: 'center',
          }}>
          <Text style={contrastTextStyle}>{title}</Text>
        </View>
        {children}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    alignItems: 'center',
    flexDirection: 'row',
    width: '80%',
    height: '80%',
    backgroundColor: colors.Neutral,
    borderRadius: 4,
  },
  verticleLine: {
    height: '90%',
    width: 1,
    backgroundColor: '#909090',
  },
  iconButtonPosition: {
    position: 'absolute',
    top: 10,
    left: 10,
  },
});

export default ModalLogo;
