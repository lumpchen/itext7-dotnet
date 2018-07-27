using System.Collections.Generic;
using iText.StyledXmlParser.Css;
using iText.StyledXmlParser.Node;

namespace iText.StyledXmlParser.Css.Selector.Item {
    internal class CssPseudoClassLastOfTypeSelectorItem : CssPseudoClassChildSelectorItem {
        private static readonly iText.StyledXmlParser.Css.Selector.Item.CssPseudoClassLastOfTypeSelectorItem instance
             = new iText.StyledXmlParser.Css.Selector.Item.CssPseudoClassLastOfTypeSelectorItem();

        private CssPseudoClassLastOfTypeSelectorItem()
            : base(CommonCssConstants.LAST_OF_TYPE) {
        }

        public static iText.StyledXmlParser.Css.Selector.Item.CssPseudoClassLastOfTypeSelectorItem GetInstance() {
            return instance;
        }

        public override bool Matches(INode node) {
            if (!(node is IElementNode) || node is ICustomElementNode || node is IDocumentNode) {
                return false;
            }
            IList<INode> children = GetAllSiblingsOfNodeType(node);
            return !children.IsEmpty() && node.Equals(children[children.Count - 1]);
        }
    }
}
