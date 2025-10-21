using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Lesta_Blitz_Cluster_Selector.Utils;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Lesta_Blitz_Cluster_Selector.Models
{
    public static class Lesta
    {
        public static readonly string RegionsConfigUrl = "http://cdn-ptl-static-2.tanksblitz.ru/tb-static/conf/regions_12.4.0_ruby.yaml";

        public static bool TryGetClusterAddresses(ref List<string> addresses)
        {
            try
            {
                string yamlConfig = ContentDownloader.DownloadTextFromUrl(RegionsConfigUrl);
                addresses = GetClusterAddressesFromYaml(yamlConfig);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static List<string> GetClusterAddressesFromYaml(string yamlConfig)
        {
            YamlStream yaml = new YamlStream();
            yaml.Load(new StringReader(yamlConfig));

            YamlNode rootNode = yaml.Documents[0].RootNode;

            return ParseClusterAddresses(rootNode);
        }

        private static List<string> ParseClusterAddresses(YamlNode node)
        {
            if (node is YamlMappingNode mappingNode &&
                mappingNode.Children.TryGetValue(new YamlScalarNode("Regions"), out var regionsNode) &&
                regionsNode is YamlMappingNode regionsMappingNode &&
                regionsMappingNode.Children.TryGetValue(new YamlScalarNode("Russia"), out var russiaNode) &&
                russiaNode is YamlMappingNode russiaMappingNode &&
                russiaMappingNode.Children.TryGetValue(new YamlScalarNode("hosts"), out var hostsNode) &&
                hostsNode is YamlMappingNode hostsMappingNode)
            {
                List<string> result = new List<string>();

                foreach (var clusterNode in hostsMappingNode.Children)
                {
                    if (clusterNode.Value is YamlMappingNode clusterMappingNode)
                    {
                        foreach (var innerNode in clusterMappingNode.Children)
                        {
                            if (innerNode.Key is YamlScalarNode keyScalarNode &&
                                keyScalarNode.Value == "url" &&
                                innerNode.Value is YamlScalarNode valueScalarNode)
                            {
                                string[] addressParts = valueScalarNode.Value.Split(':');

                                result.Add(addressParts[0]);
                            }
                        }
                    }
                }

                return result;
            }

            throw new InvalidOperationException("Invalid YAML structure.");
        }
    }
}
