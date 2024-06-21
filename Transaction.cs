using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CryptoTransactionTracker
{
    public class Transaction
    {
        [JsonProperty("subnetwork_id")]
        public string SubnetworkId { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("mass")]
        public long Mass { get; set; }

        [JsonProperty("block_hash")]
        public List<string> BlockHash { get; set; }

        [JsonProperty("block_time")]
        public long BlockTime { get; set; }

        [JsonProperty("is_accepted")]
        public bool IsAccepted { get; set; }

        [JsonProperty("accepting_block_hash")]
        public string AcceptingBlockHash { get; set; }

        [JsonProperty("accepting_block_blue_score")]
        public long AcceptingBlockBlueScore { get; set; }

        [JsonProperty("inputs")]
        public List<Input> Inputs { get; set; }

        [JsonProperty("outputs")]
        public List<Output> Outputs { get; set; }
    }

    public class Input
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("previous_outpoint_hash")]
        public string PreviousOutpointHash { get; set; }

        [JsonProperty("previous_outpoint_index")]
        public string PreviousOutpointIndex { get; set; }

        [JsonProperty("previous_outpoint_resolved")]
        public object PreviousOutpointResolved { get; set; }

        [JsonProperty("previous_outpoint_address")]
        public object PreviousOutpointAddress { get; set; }

        [JsonProperty("previous_outpoint_amount")]
        public object PreviousOutpointAmount { get; set; }

        [JsonProperty("signature_script")]
        public string SignatureScript { get; set; }

        [JsonProperty("sig_op_count")]
        public string SigOpCount { get; set; }
    }

    public class Output
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("transaction_id")]
        public string TransactionId { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("script_public_key")]
        public string ScriptPublicKey { get; set; }

        [JsonProperty("script_public_key_address")]
        public string ScriptPublicKeyAddress { get; set; }

        [JsonProperty("script_public_key_type")]
        public string ScriptPublicKeyType { get; set; }

        [JsonProperty("accepting_block_hash")]
        public object AcceptingBlockHash { get; set; }
    }
}

