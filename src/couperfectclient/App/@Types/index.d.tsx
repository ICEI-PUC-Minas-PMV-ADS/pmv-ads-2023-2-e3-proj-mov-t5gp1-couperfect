declare module "*.svg"{
    import { SvgProps } from 'react-native-svg';

    const Content: React.FC<SvgProps>;

    export default Content;
}