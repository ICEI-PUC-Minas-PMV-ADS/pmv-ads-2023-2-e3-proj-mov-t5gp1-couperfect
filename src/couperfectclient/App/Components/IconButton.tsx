import React from 'react';
import { GestureResponderEvent, Pressable, StyleSheet, ViewStyle } from 'react-native';
import ArrowReturnLeftSvg from '../Assets/ArrowReturnLeft.svg';
import EyeSvg from '../Assets/Eye.svg';
import EyeSlashSvg from '../Assets/EyeSlash.svg';
import { colors } from '../Styles';

interface BackButtonProps {
    style?: ViewStyle;
    type: 'eye' | 'back';
    onPress?: ((event: GestureResponderEvent) => void);
    isEyeOpen?: boolean;
    setEyeOpen?: React.Dispatch<React.SetStateAction<boolean>>
}

const IconButton: React.FC<BackButtonProps> = ({ style, type, onPress, isEyeOpen, setEyeOpen }) => {
    function internalOnPress(event: GestureResponderEvent) {
        if (type === 'eye' && setEyeOpen) setEyeOpen(isOpen => !isOpen);
        if (onPress) onPress(event);
    }

    const InternalIcon: React.FC = () => 
    {
        if(type === 'eye')
            return isEyeOpen ? <EyeSvg/> : <EyeSlashSvg/>;

        return <ArrowReturnLeftSvg />;
    }

    const combineStyle = StyleSheet.compose(defaultStyle, style);

    return (
        <Pressable onPress={internalOnPress} style={combineStyle}>
            <InternalIcon />
        </Pressable>
    );
}

const defaultStyle: ViewStyle = {
    width: 40,
    height: 40,
    backgroundColor: colors.PrimaryContrast,
    borderRadius: 4,
    borderWidth: 1,
    borderColor: colors.NeutralContrast,
    justifyContent: 'center',
    alignItems: 'center',
};

export default IconButton;